using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiChoThue.Models;

namespace DiChoThue.Services
{
    public interface IDonHangService
    {
        Task<List<DonHang>> GetAllAsync();
        Task<DonHang> GetByIdAsync(string id);
        Task<DonHang> CreateAsync(DonHang donHang);
        Task UpdateAsync(string id, DonHang donHang);
        Task DeleteAsync(string id);
    }
}
