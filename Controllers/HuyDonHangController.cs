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
using DiChoThue.Services;
using Microsoft.Extensions.Options;

namespace DiChoThue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuyDonHangController : ControllerBase
    {
        private readonly IMongoCollection<DonHang> _collection;
        private readonly DbConfiguration _settings;
        public HuyDonHangController(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<DonHang>(_settings.CollectionName);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> HuyDonHang(string id)
        {
            var donHang = await _collection.Find(c => c.MaDonHang == id).FirstOrDefaultAsync().ConfigureAwait(false);
            if (donHang == null)
            {
                return NotFound();
            }
            donHang.TrangThai = "Đã huỷ";
            var updatedDonHang = await _collection.ReplaceOneAsync(c => c.MaDonHang == id, donHang).ConfigureAwait(false);
            return NoContent();
        }
    }
}