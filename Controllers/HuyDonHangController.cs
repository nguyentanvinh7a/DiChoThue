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
using Microsoft.AspNetCore.Cors;

namespace DiChoThue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class HuyDonHangController : ControllerBase
    {
        private readonly IMongoCollection<DonHang> _collection;
        private readonly DbConfiguration _settings;
        public HuyDonHangController(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<DonHang>("DonHang");
        }

        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> HuyDonHang(string id)
        {
            var donHang = await _collection.Find(c => c._id == (new ObjectId(id))).FirstOrDefaultAsync().ConfigureAwait(false);
            Console.WriteLine("Don hang ne: ", donHang);
            if (donHang == null)
            {
                return NotFound();
            }
            donHang.tinhTrang = 2;
            var updatedDonHang = await _collection.ReplaceOneAsync(c => c._id == (new ObjectId(id)), donHang).ConfigureAwait(false);
            return Ok(donHang);
        }
    }
}