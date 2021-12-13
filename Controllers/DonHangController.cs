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
    public class DonHangController : ControllerBase
    {
        private readonly IMongoCollection<DonHang> _collection;
        private readonly DbConfiguration _settings;
        public DonHangController(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<DonHang>("DonHang");
        }

        [HttpGet]
        public Task<List<DonHang>> GetAllAsync()
        {
            return _collection.Find(c => true).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<DonHang> Get(string id)
        {
            var donHang = await _collection.Find(c => c.maDonHang == id).FirstOrDefaultAsync().ConfigureAwait(false);
            return donHang;
        }

        [HttpPost]
        public async Task<DonHang> CreateAsync(DonHang donHang)
        {
            await _collection.InsertOneAsync(donHang).ConfigureAwait(false);
            return donHang;
        }
        [HttpPut("/HuyDonHang/{id}")]
        public async Task<IActionResult> HuyDonHang(string id)
        {
            var donHang = await _collection.Find(c => c.maDonHang == id).FirstOrDefaultAsync().ConfigureAwait(false);
            if (donHang == null)
            {
                return NotFound();
            }
            donHang.trangThai = "Đã huỷ";
            var updatedDonHang = await _collection.ReplaceOneAsync(c => c.maDonHang == id, donHang).ConfigureAwait(false);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, DonHang donHangIn)
        {
            var donHang = await _collection.Find(c => c.maDonHang == id).FirstOrDefaultAsync().ConfigureAwait(false);
            if (donHang == null)
            {
                return NotFound();
            }
            donHangIn.id = donHang.id;
            donHangIn.maDonHang = donHang.maDonHang;
            if (donHangIn.thoiGianDat == null) donHangIn.thoiGianDat = donHang.thoiGianDat;
            if (donHangIn.trangThai == null) donHangIn.trangThai = donHang.trangThai;
            var updatedDonHang = await _collection.ReplaceOneAsync(c => c.maDonHang == id, donHangIn).ConfigureAwait(false);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var donHang = await _collection.Find(c => c.maDonHang == id).FirstOrDefaultAsync().ConfigureAwait(false);
            if (donHang == null)
            {
                return NotFound();
            }

            var updatedDonHang = await _collection.DeleteOneAsync(c => c.maDonHang == id).ConfigureAwait(false);
            return NoContent();
        }
    }
}