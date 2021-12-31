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
    public class ChiTietDonHangController : ControllerBase
    {
        private readonly IMongoCollection<ChiTietDonHang> _collection;
        private readonly DbConfiguration _settings;
        public ChiTietDonHangController(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<ChiTietDonHang>("ChiTietDonHang");
        }

        [HttpGet]
        public Task<List<ChiTietDonHang>> GetAll()
        {
            return _collection.Find(c => true).ToListAsync();
        }
        [HttpGet("{donHang}")]
        public Task<List<ChiTietDonHang>> Get(string donHang)
        {
            return _collection.Find(c => c.donHang == (new ObjectId(donHang))).ToListAsync();
        }

        [HttpPost]
        public async Task<ChiTietDonHang> CreateAsync(ChiTietDonHang chiTietDonHang)
        {
            await _collection.InsertOneAsync(chiTietDonHang).ConfigureAwait(false);
            return chiTietDonHang;
        }        
    }
}