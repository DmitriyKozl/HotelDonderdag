﻿using HotelProject.BL.Model;

namespace Hotel.BL.Model;

public class Registration {
    public Registration(Customer customer, Activity activity) {
   
        _customer = customer;
        _activity = activity;
        NumberOfAdults = 1; 
        AdultOrChild(customer);
        CalculatePrice();
    }
    
    public int Id {
        get { return _id; }
        set {
            if (value <= 0) throw new RegistrationException("invalid id");
            _id = value;
        }
    }
    private int _id;
    
    public Customer Customer {
        get { return _customer; }
        set {
            if (value == null) throw new RegistrationException("customer null");
            _customer = value;
        }
    }
    private Customer _customer;
    
    public Activity Activity {
        get { return _activity; }
        set {
            if (value == null) throw new RegistrationException("activity null");
            _activity = value;
        }
    }
    private Activity _activity;
    
    public decimal Price {
        get { return _price; }
        set {
            if (value <= 0) throw new RegistrationException("invalid price");
            _price = value;
        }
    }
    private decimal _price;
    
    public decimal costChild {
        get { return _costChild; }
        set {
            if (value < 0) throw new RegistrationException("invalid costchild");
            _costChild = value;
        }
    }
    private decimal _costChild;
    
    public decimal costAdult {
        get { return _costAdult; }
        set {
            if (value < 0) throw new RegistrationException("invalid costadult");
            _costAdult = value;
        }
    }
    private decimal _costAdult;
    
    public int NumberOfAdults {
        get { return _numberOfAdults; }
        set {
            if (value <= 0) throw new RegistrationException("invalid numberofadults");
            _numberOfAdults = value;
        }
    }
    private int _numberOfAdults;
    
    
    public int NumberOfChildren {
        get { return _numberOfChildren; }
        set {
            if (value < 0) throw new RegistrationException("invalid numberofchildren");
            _numberOfChildren = value;
        }
    }
    private int _numberOfChildren;
    
    private void CalculatePrice() {
        if (_activity.Discount != null || _activity.Discount != 0) {
            costAdult =  (_activity.PriceAdult - (_activity.PriceAdult * (_activity.Discount / 100))) *
                        _numberOfAdults;
            if (Customer.Members.Count != 0)
                costChild = (_activity.PriceChild - (_activity.PriceChild * (_activity.Discount / 100))) *
                            _numberOfChildren;
            else
                costChild = 0;
        }
        else {
            costAdult = _activity.PriceAdult * _numberOfAdults;
            if (Customer.Members.Count != 0) costChild = _activity.PriceChild * _numberOfChildren;
            else costChild = 0;
        }
        costAdult = decimal.Parse(costAdult.ToString("0.00"));
        costChild = decimal.Parse(costChild.ToString("0.00"));
        Price = decimal.Parse((costAdult + costChild).ToString("0.00"));
    }
    
    private void AdultOrChild(Customer customer) {
        foreach (var member in customer.Members) {
            if (DateTime.Now.Year - member.Birthday.Year >= 18) NumberOfAdults++;
            else NumberOfChildren++;
        }
    }

}