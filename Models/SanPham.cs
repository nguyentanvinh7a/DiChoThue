using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiChoThue.Models
{
	public class SanPham
	{
		[BsonId]
		public ObjectId _id { get; set; }
		[BsonElement]
		public string tenSanPham { get; set; }
		[BsonElement]
		public string xuatXu { get; set; }
		[BsonElement]
		public long giaTien { get; set; }

		[BsonElement]
		public DateTime hanSuDung { get; set; }
		[BsonElement]
		public ObjectId cuaHang { get; set; }
		[BsonElement]
		public ObjectId loaiHang { get; set; }
		[BsonElement]
		public string hinhAnh { get; set; }
		[BsonElement]
		public Boolean thietYeu { get; set; }
		[BsonElement]
		public string tenCuaHang { get; set; }
		[BsonElement]
		public string tenLoaiHang { get; set; }
		[BsonElement]
		public string donViTinh { get; set; }

	}
}
