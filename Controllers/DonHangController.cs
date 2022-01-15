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
        public Task<List<DonHang>> GetAll()
        {
            return _collection.Find(c => true).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<DonHang> Get(string id)
        {
            var donHang = await _collection.Find(c => c._id.ToString() == id).FirstOrDefaultAsync().ConfigureAwait(false);
            return donHang;
        }

        [HttpPost]
        
        public async Task<DonHang> CreateAsync(DonHang donHang)
        {
            await _collection.InsertOneAsync(donHang).ConfigureAwait(false);
            return donHang;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, DonHang donHangIn)
        {
            var donHang = await _collection.Find(c => c._id.ToString() == id).FirstOrDefaultAsync().ConfigureAwait(false);
            if (donHang == null)
            {
                return NotFound();
            }
            donHangIn._id = donHang._id;
            if (donHangIn.thoiGianDat == null) donHangIn.thoiGianDat = donHang.thoiGianDat;
            if (donHangIn.tinhTrang == null) donHangIn.tinhTrang = donHang.tinhTrang;
            var updatedDonHang = await _collection.ReplaceOneAsync(c => c._id.ToString() == id, donHangIn).ConfigureAwait(false);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var donHang = await _collection.Find(c => c._id.ToString() == id).FirstOrDefaultAsync().ConfigureAwait(false);
            if (donHang == null)
            {
                return NotFound();
            }

            var updatedDonHang = await _collection.DeleteOneAsync(c => c._id.ToString() == id).ConfigureAwait(false);
            return NoContent();
        }
    }
}