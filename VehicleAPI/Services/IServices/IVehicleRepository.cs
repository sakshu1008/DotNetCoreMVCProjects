using VehicleAPI.Models.DTO;

namespace VehicleAPI.Services.IServices
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<VehicleDto>> GetAllAsync();
        Task<VehicleDto> GetByIdAsync(int id);
        Task<int> CreateVehicleAsync(CreateVehicleDto vehicle);
        Task<int> UpdateVehicleAsync(UpdateVehicleDto vehicle);
        Task<int> DeleteVehicleAsync(int id);

    }
}
