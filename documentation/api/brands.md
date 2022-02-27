# Brands

You can manage brands as part of the inventory management of the API. From here it will be describe the API end points on managing brands.

As part of the role managements. Only users with the **UserType** `Admin` can Create, Edit, or Update a Brand. For now, there is no option to delete a brand.



## Endpoints

Any user can search brand by ID using the following end point:
```
brand/<brand id>
```

Any user can get all brands by using the following endpoint:
```
brand/
```
`NOTE: it will list all brands`

`TODO: Do Pagination with getallbrands`

Only users with **UserType** `Admin` can create a brand. Endpoint:
```
brand/
```
Users must send a *Post* `json` request with the body [BrandDto](../../src/cabzcommerce.cshared/DTOs/Product/BrandDto.cs) model.

Only Users with **UserType** `Admin` can update a brand.
Endpoint:
```
brand/<brand id>
```
Users must do a *Put* `json` request with the body [BrandDto](../../src/cabzcommerce.cshared/DTOs/Product/BrandDto.cs) model.
