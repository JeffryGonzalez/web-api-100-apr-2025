# Vendors


Creating a Commercial Vendor

`POST /commercial-vendors`

```json
{
    "name": "Jetbrains",
    "site": "https://jetbrains.com",
    "contactFirstName": "Khalid",
    "contactLastName": "Abernathy",
    "contactEmail": "khalid@jetbrains.com",
    "contactPhone": "888 123-456"
}
```


Response:

`201 Created`
`Location: /commercial-vendors/{id}`

```json
{
    "id": 389038983989389,
    "name": "Jetbrains",
    "site": "https://jetbrains.com",
    "addedBy": "id of manager",
    "addedOn": "date time offset of when added",
    "poc": {
        "name": {
            "first": "Khalid",
            "last": "Abernathy"            
        },
        "contactMechanisms": {
               
                "primary_email": "bob@aol.com"
            
        }
    }
}
```