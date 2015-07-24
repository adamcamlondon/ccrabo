Congestion Charge Solution

I have built the solution with extensibility in mind.  There is flexibility with the configuration of rates for specific days and vehicle type.
All rates are calculated on a Day By Day basis. 
I have used Flag enums for Vehicle Type and Day.  Day has been grouped into Weekdays and Weekends. Vehicle has been grouped into Full rate and Discount Rate.
There is no validation on the Rates as they are inserted, I’ve made the assumption this is out of scope.
The solution does however cater for over-lapping Rates.  For instance if you had two rates for weekday 12:00 – 19:00, and one has Vehicle Type “Car” and the other “FullRate” 
it will choose the more specific Car instance.  The same logic works for individual Days vs the less specific Weekday / Weekend.
Preference is given to specific Vehicle Type then Weekday.  If all else fails it will simply pick the first entry it finds.

Compromises:
The rate picking logic at present is simple, any overlap between rates and it will excluded one, even if the overlap is for a minute. And it’s possible for one Rate to overwrite multiple rates that had no overlap.
The Spec flow testing works however it’s a little messy due to time constraints.
The structure of the library isn’t ideal and is very loosely based on having objects that may one day be exposed in Model with Library containing static helper classes and extension methods.
I probably would have made the Invoice type a single entity rather than a collection for extensibility (which would have made injecting the InvoiceBuilder class more practical).
 However I left it as is to conform to the Feature File.

