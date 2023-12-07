using Hotel.BL.Model;

namespace Hotel.BL.Interfaces; 

public interface IRegistrationRepository {
    void AddRegistration(Registration registration);
}