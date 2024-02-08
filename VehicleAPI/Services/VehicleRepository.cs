using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VehicleAPI.Data;
using VehicleAPI.Models;
using VehicleAPI.Models.DTO;
using VehicleAPI.Services.IServices;

namespace VehicleAPI.Services
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleDbContext _context;
        private readonly IMapper _mapper;
        public VehicleRepository(VehicleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> CreateVehicleAsync(CreateVehicleDto vehicle)
        {
            if(vehicle == null)
            {
                throw new Exception("Add vehicle page not found! Please try again...");
            }
            Vehicle VehicleToAdd = _mapper.Map<Vehicle>(vehicle);
            VehicleToAdd.LastUpdated = DateTime.Now;
            await _context.Vehicles.AddAsync(VehicleToAdd);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteVehicleAsync(int id)
        {
            if(id == null)
            {
                throw new Exception("Delete vehicle page not found! Please try again...");
            }
            Vehicle VehicleToDelete = _mapper.Map<Vehicle>(await _context.Vehicles.FindAsync(id));
            if(VehicleToDelete == null)
            {
                throw new Exception("Vehicle data not found! Please try again...");
            }
            _context.Vehicles.Remove(VehicleToDelete);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
        {
            IEnumerable<Vehicle> VehicleList = await _context.Vehicles.ToListAsync();
            if(VehicleList == null)
            {
                throw new Exception("Vehicle List not found! Please try again...");
            }
            return _mapper.Map<IEnumerable<VehicleDto>>(VehicleList);
        }

        public async Task<VehicleDto> GetByIdAsync(int id)
        {
            if (id == null)
            {
                throw new Exception("Get vehicle by Id page not found! Please try again...");
            }
            var VehicleById = await _context.Vehicles.FindAsync(id);
            return _mapper.Map<VehicleDto>(VehicleById);
        }

        public async Task<int> UpdateVehicleAsync(UpdateVehicleDto vehicle)
        {
            var vehicleToUpdate = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == vehicle.Id);
            if(vehicleToUpdate == null)
            {
                throw new Exception("Update vehicle page not found! Please try again...");
            }
            vehicleToUpdate.Name = vehicle.Name;
            vehicleToUpdate.Description = vehicle.Description;
            vehicleToUpdate.LaunchDate = vehicle.LaunchDate;
            vehicleToUpdate.CompanyName = vehicle.CompanyName;
            var UpdatedVehicle = _mapper.Map<Vehicle>(vehicleToUpdate);
            UpdatedVehicle.LastUpdated = DateTime.Now;
            _context.Vehicles.Update(vehicleToUpdate);
            return await _context.SaveChangesAsync();
        }
    }
}
