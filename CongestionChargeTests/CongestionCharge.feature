Feature: CongestionCharge
	In order to pay for a congestion charge
	As a driver
	I want to be told the Congestion Charge for a set period and vehicle type

Background: 
Given The Congestion Rates Are
	| Description | Day     | Vehicle            | Start | End		| Rate |
	| AM rate   | Weekday | FullRateVehicle    | 07:00 | 12:00    | 2.0  |
	| PM rate   | Weekday | FullRateVehicle    | 12:00 | 19:00    | 2.5  |
	| AM rate   | Weekday | DicountRateVehicle | 07:00 |  12:00   | 1.0  |
	| PM rate   | Weekday | DicountRateVehicle | 12:00 |  19:00   | 1.0  |


Scenario: Car: 24/04/2008 11:32 - 24/04/2008 14:42
	Given I am driving a car
	When I enter the congestion charge zone at 24/04/2008 11:32
	And I leave the congestion charge zone at 24/04/2008 14:42
	Then I should get the following:
	| Description                  | Value |
	| Charge for 0h 28m (AM rate): | £0.90 |
	| Charge for 2h 42m (PM rate): | £6.70 |
	| Total Charge:                | £7.60 |


Scenario: Motorbike: 24/04/2008 17:00 - 24/04/2008 22:11
	Given I am driving a Motorbike
	When I enter the congestion charge zone at 24/04/2008 17:00
	And I leave the congestion charge zone at 24/04/2008 22:11
	Then I should get the following:
	| Description                 | Value |
	| Charge for 0h 0m (AM rate): | £0.00 |
	| Charge for 2h 0m (PM rate): | £2.00 |
	| Total Charge:               | £2.00 |

Scenario: Van: 25/04/2008 10:23 - 28/04/2008 09:02
	Given I am driving a Van
	When I enter the congestion charge zone at 25/04/2008 10:23
	And I leave the congestion charge zone at 28/04/2008 09:02
	Then I should get the following:
	| Description                  | Value    |
	| Charge for 3h 39m (AM rate): | £7.30    |
	| Charge for 7h 0m (PM rate):  | £17.50   |
	| Total Charge:                | £24.80   |