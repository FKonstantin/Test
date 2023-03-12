using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestFisenko.Data;
using TestFisenko.Models;

namespace TestFisenko.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : Controller
    {
        private readonly ApiContext _context;
        
        public GoodsController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создаёт товар в БД
        /// </summary>
        /// <param name="Goods"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult Create(Goods Goods)
        {
            if (Goods.Id == 0)
            {
                _context.GoodsOrder.Add(Goods);
                try 
                {
                    _context.SaveChanges();
                    return new JsonResult(Ok(Goods));
                }
                catch { }
            }

            return new JsonResult(NoContent());
        }

        /// <summary>
        /// Возвращает все товары из БД
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public JsonResult GetAll()
        {
            var result = _context.GoodsOrder.ToList();

            return new JsonResult(Ok(result));
        }

        /// <summary>
        /// Возвращает 1 товар из БД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetById(int id)
        {
            var result = _context.GoodsOrder.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return Ok(result);
        }

        /// <summary>
        /// Редактирует товар в БД
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public JsonResult Edit(Goods Goods)
        {
            try 
            {
                _context.GoodsOrder.Update(Goods);
                _context.SaveChanges();

                return new JsonResult(Ok(Goods));
            }
            catch 
            {
                return new JsonResult(NotFound());
            }
        }

        /// <summary>
        /// Загружает файл в локальную файловую систему
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            Image image = new Image();
            //--- Получаем относительный путь до папки с программой
            string filePath = Environment.CurrentDirectory.ToString();
            //--- Получаем расширение файла
            FileInfo fileExtension = new FileInfo(file.FileName);
            //--- Создаём уникальное имя файла
            Guid g = Guid.NewGuid();
            image.NameImage = g.ToString();
            //--- Получаем путь к файлу на диске
            image.PathImage = filePath + "\\GoodsImages\\" + image.NameImage + fileExtension;

            if (file != null)
            {
                //--- Сохраняет файл в локальную файловую систему, используя имя файла, созданное приложением
                using (var stream = System.IO.File.Create(image.PathImage))
                {
                    await file.CopyToAsync(stream);
                }
            }

            //--- Сохранение в БД информации о файле
            _context.GoodsImage.Add(image);
            _context.SaveChanges();

            return Ok(new { path_image = image.PathImage, id_image = image.Id, name_image = image.NameImage });
        }
    }
} 
