using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiChoThue.Models
{
    public class ChiTietDonHang
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public ObjectId donHang { get; set; }
        [BsonElement]
        public int soLuong { get; set; }
        [BsonElement]
        public ObjectId sanPham { get; set; }
    }
}
