using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiChoThue.Models
{
    public class DonHang
    {
        [BsonId]
        public ObjectId id { get; set; }
        [BsonElement]
        public string maDonHang { get; set; }
        [BsonElement]
        public string trangThai { get; set; }
        [BsonElement]
        public string thoiGianDat { get; set; }
    }
}
