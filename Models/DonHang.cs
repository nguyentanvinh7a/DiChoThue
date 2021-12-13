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
        public ObjectId _id { get; set; }
        [BsonElement]
        public int tinhTrang { get; set; }
        [BsonElement]
        public DateTime thoiGianDat { get; set; }
        [BsonElement]
        public ObjectId nguoiMua { get; set; }
        [BsonElement]
        public ObjectId phuongThucThanhToan { get; set; }
        [BsonElement]
        public ObjectId shipper { get; set; }
    }
}
