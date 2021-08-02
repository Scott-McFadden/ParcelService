# ParcelService
This is a class library that helps .net core apps integrate with the Regrid.com API


Note: A subscription to Regrid.com API Library is required before this will work. See https://app.regrid.com/api/parcel-api for subscription details.


## Usage:

ParcelService.ParcelService ps = new ParcelService.ParcelService(log, SecurityToken);

string ret = await ps.GetParcelByCoordinatesAsync(TestLon, TestLat);



