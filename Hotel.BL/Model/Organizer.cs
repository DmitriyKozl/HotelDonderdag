using HotelProject.BL.Exceptions;

namespace Hotel.BL.Model;

public class Organizer {
    public Organizer(int organizerId, string name, string description, ContactInfo contactInfo) {
        OrganizerId = organizerId;
        Name = name;
        Description = description;
        ContactInfo = contactInfo;
    }

    private int _organizerId;
    private string _name;
    private string _description;
    private ContactInfo _contactInfo;

    public int OrganizerId {
        get { return _organizerId; }
        set {
            if (value <= 0) throw new OrganizerException("invalid id");
            _organizerId = value;
        }
    }

    public string Name {
        get { return _name; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new OrganizerException("name is empty");
            _name = value;
        }
    }

    public string Description {
        get { return _description; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new OrganizerException("description is empty");
            _description = value;
        }
    }

    public ContactInfo ContactInfo {
        get { return _contactInfo; }
        set {
            if (value == null) throw new OrganizerException("contactinfo null");
            _contactInfo = value;
        }
    }
  
    public override string ToString() {
        return $"OrganizerId: {OrganizerId}, Name: {Name}, Description: {Description}, ContactInfo: {ContactInfo}";
    }
}