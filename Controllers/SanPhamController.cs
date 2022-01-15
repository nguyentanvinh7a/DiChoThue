using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DiChoThue.Data;
using DiChoThue.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using System.IO;
using OfficeOpenXml;

namespace DiChoThue.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SanPhamController : ControllerBase
	{
		private readonly IMongoCollection<SanPham> _collection;
		private readonly DbConfiguration _settings;
		public SanPhamController(IOptions<DbConfiguration> settings)
		{
			_settings = settings.Value;
			var client = new MongoClient(_settings.ConnectionString);
			var database = client.GetDatabase(_settings.DatabaseName);
			_collection = database.GetCollection<SanPham>("SanPham");
		}
		[HttpGet]
		public Task<List<SanPham>> GetAll()
		{
			return _collection.Find(c => true).ToListAsync();
		}
		[HttpGet("{id}")]
		public async Task<SanPham> Get(string id)
		{
			var sanPham = await _collection.Find(c => c._id.ToString() == id).FirstOrDefaultAsync().ConfigureAwait(false);
			return sanPham;
		}
		[HttpGet("thietyeu")]
		public Task<List<SanPham>> thietyeu()
		{
			return _collection.Find(c => c.thietYeu == true).ToListAsync();
		}
		[HttpGet("timkiem/{tensp}")]
		public List<SanPham> TimKiem(string tensp)
		{
			return _collection.AsQueryable<SanPham>().AsEnumerable().Where(c => tensp.All(key => c.tenSanPham.Contains(key))).ToList();
		}
		[HttpPost]
		[Route("upload")]
		public async Task<List<SanPham>> importProduct(IFormFile file)
		{
			try
			{
				var listProduct = new List<SanPham>();
				using (var stream = new MemoryStream())
				{
					await file.CopyToAsync(stream);
					using (var package = new ExcelPackage(stream))
					{
						var worksheet = package.Workbook.Worksheets[0];
						var rowcount = worksheet.Dimension.Rows;
						for (var row = 2; row <= rowcount; row++)
						{
							listProduct.Add(new SanPham
							{
								tenSanPham = worksheet.Cells[row, 1].Value.ToString().Trim(),
								xuatXu = worksheet.Cells[row, 2].Value.ToString().Trim(),
								giaTien = Int32.Parse(worksheet.Cells[row, 3].Value.ToString().Trim()),
								hanSuDung = DateTime.Parse(worksheet.Cells[row, 4].Value.ToString().Trim()),
								hinhAnh = worksheet.Cells[row, 5].Value.ToString().Trim()
							});
						}
					}
				}
				return listProduct;
			}
			catch
			{
				throw;
			}
		}
	}
}
