using HotelProject.BL.Exceptions;

namespace Hotel.BL.Model;

public class Activity {
    public Activity(string name, string description, DateTime date, int duration, int capacity,
        decimal priceAdult, decimal priceChild, decimal discount, string location) {
        Name = name;
        Description = description;
        Date = date;
        Duration = duration;
        Capacity = capacity;
        PriceAdult = priceAdult;
        PriceChild = priceChild;
        Discount = discount;
        Location = location;
    }

    public Activity(int activityId, string name, string description, DateTime date, int duration, int capacity,
        decimal priceAdult, decimal priceChild, decimal discount, string location)
        : this(name, description, date, duration, capacity, priceAdult, priceChild, discount, location) {
        ActivityId = activityId;
    }

    private int _activityId;
    private string _name;
    private string _description;
    private DateTime _date;
    private int _duration;
    private int _capacity;
    private decimal _priceAdult;
    private decimal _priceChild;
    private decimal _discount;
    private string _location;

    public int ActivityId {
        get { return _activityId; }
        set {
            if (value <= 0) throw new ActivityException("invalid id");
            _activityId = value;
        }
    }

    public string Name {
        get { return _name; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new ActivityException("name is empty");
            _name = value;
        }
    }

    public string Description {
        get { return _description; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new ActivityException("description is empty");
            _description = value;
        }
    }

    public DateTime Date {
        get { return _date; }
        set {
            if (value == null) throw new ActivityException("date is null");
            _date = value;
        }
    }

    public int Duration {
        get { return _duration; }
        set {
            if (value < 0) throw new ActivityException("invalid duration");
            _duration = value;
        }
    }

    public int Capacity {
        get { return _capacity; }
        set {
            if (value < 0) throw new ActivityException("invalid capacity");
            _capacity = value;
        }
    }

    public decimal PriceAdult {
        get { return _priceAdult; }
        set {
            if (value <= 0) throw new ActivityException("invalid priceadult");
            _priceAdult = value;
        }
    }

    public decimal PriceChild {
        get { return _priceChild; }
        set {
            if (value < 0) throw new ActivityException("invalid pricechild");
            _priceChild = value;
        }
    }

    public decimal Discount {
        get { return _discount; }
        set {
            if (value < 0 || value > 100) throw new ActivityException("invalid discount");
            _discount = value;
        }
    }

    public string Location {
        get { return _location; }
        set {
            if (string.IsNullOrWhiteSpace(value)) throw new ActivityException("invalid location");
            _location = value;
        }
    }

    public override string ToString() {
        return $"{ActivityId}: {Name}, Description: {Description}, Location: {Location}, " +
               $"Date: {Date}, Duration: {Duration}, Capacity: {Capacity}, " +
               $"Adult Price: {PriceAdult}, Child Price: {PriceChild}, Discount: {Discount}";
    }
}