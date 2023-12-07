using Hotel.BL.Interfaces;
using Hotel.BL.Model;

namespace Hotel.BL.Managers; 

public class OrganizerManager {
    private IOrganiserRepository _organizerRepository;

    public OrganizerManager(IOrganiserRepository organizerRepository)
    {
        _organizerRepository = organizerRepository;
    }

    public List<Organizer> GetOrganizers()
    {
        return _organizerRepository.GetOrganizers();
    }
}