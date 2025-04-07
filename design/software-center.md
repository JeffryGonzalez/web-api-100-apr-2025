# Software Center

The Software Center at our company maintains the list of currently supported software for our enterprise.

Supported software can be requested by employees and will be automatically entitled and installed on their machines upon request.

## The API

We are building an API that will streamline the management of software catalog items, vendors, and, eventually, issues raised by the employees through the support staff that may need to be addressed by the vendors.

### Vendors

The API must allow for the creation an maintainence of software vendors.

Vendors can either be:

- Commercial
- Open Source
- In House

#### Commercial Vendors

Companies that sell and provide software and support for that software. For commercial vendors, we need to track:

- The name of the vendor
- Their Internet URL
- A POC - name, email, phone, if possible.
- The list of catalog items provided by that vendor

(Note: for purposes of this training, we aren't tracking costs, etc.)

#### Open Source

For approved OpenSource software and libraries, we need to track:

- The name of the organization, if any, that "owns" the software, library, framework, etc.
- An internet URL of documentation, if available
- A link to the source code repository for this provider.
- A link to, and an indication of the license (MIT, GPL, etc.)
- A list of the catalog items associated with this vendor/provider.

#### In-House

Some software we use is created in-house, and maintained internally.

We need:
- The name of the team that maintains the software
- A contact (name, phone, email) for the owner of this app.
- The list of items associated with this team.

## Vendor Notes

Commercial Vendors must be approved by EA before being listed.

## Catalog Items

Catalog items are *always* associated with vendor/provider. No item can be added to the catalog that does not have a provider.

Catalog Items each *may* have:

- Specific versions (e.g. 2.31.0, 2022, etc.)
  - note, if version is based on [SEMVER](https://semver.org) then only the major and minor will be listed, we will also install the latest available in patch for the requested minor/major)
- Specific Platforms (versions may vary based on platform) (e.g. MacOs, Windows11, Windows10, Linux, iOs, Android)

If a version and/or platform isn't provided in for the catalog item, we will assume it can be requested, and the requester will 
receive whatever the *current* version for their platform by the installer script.

Catalog Items can be added by members of the `SoftwareCenter` team, but cannot be listed until a `Manager` of the `SoftwareCenter` team approves them.

## Read Models

### Software Center

Members of the Software center need to be able to retrieve lists of vendors. For vendors, they need to see a list of catalog items 
associated with that vendor.

### Employees
Employees need to be able to see and search for particular catalog items. They *may* need to see a list of vendors to refine their search.

They should also be able to see lists based on things like:

- I want to see all the software available from a vendor on the MacOs Platform
- I want to see all the software as above, but only titles I either have or do not have entitled for my account.*



