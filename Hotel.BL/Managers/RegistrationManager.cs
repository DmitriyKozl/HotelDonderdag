using Hotel.BL.Interfaces;
using Hotel.BL.Model;

namespace Hotel.Domain.Managers; 

public class RegistrationManager {
    private IRegistrationRepository registrationRepository;
    public RegistrationManager(IRegistrationRepository registrationRepository)
    {
        this.registrationRepository = registrationRepository;
    }
    public void AddRegistration(Registration registration)
    {
        registrationRepository.AddRegistration(registration);
    }
}