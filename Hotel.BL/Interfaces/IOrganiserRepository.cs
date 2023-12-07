using Hotel.BL.Model;

namespace Hotel.BL.Interfaces; 

public interface IOrganiserRepository {
    
    List<Organizer> GetOrganizers();
    
}