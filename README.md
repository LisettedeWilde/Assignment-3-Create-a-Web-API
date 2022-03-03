# Assignment 3: Create a Web API

![GitHub repo size](https://img.shields.io/github/repo-size/lisettedewilde/Assignment-3-Create-a-Web-API)

[Assignment 3 PDF](https://lms.noroff.no/pluginfile.php/184749/mod_assign/introattachment/0/Assignment%203_CSharp_Web%20API%20creation%20in%20ASP.NET%20Core.pdf?forcedownload=1)

## Table of Contents

- [General Information](#general-information)
- [Technologies](#technologies)
- [Installation and Usage](#installation-and-usage)
- [Contributors](#contributors)

## General Information

The project consists of Appendix A and Appendix B:

Appendix A:
Use Entity Framework Code First to create a database with the following minimum requirements:

a) Create models and DbContext to cater for the specifications in Appendix A.

b) Proper configuration of datatypes is to be done using data attributes.

c) Comments on each of the classes showing where navigation properties are and aspects of DbContext.

Appendix B:
Create a Web API in ASP.NET Core with the following minimum requirements:

a) Create controllers and DTOs according to the specifications in Appendix B.

b) Swagger/Open API documentation using annotations.

c) Summary (///) tags for each method you write, explaining what the method does, any
exceptions it can throw and what data it returns (if applicable).

## Technologies

The project is implemented with following technologies:

- C#

- .NET 5.0

- SQL Server

- ASP.NET Core

- Entity Framework Core

## Installation and Usage

**NOTE:** For refference to the project, please read the assignment provided at the top of this readme.

1. Clone the project repository

```sh
git clone https://github.com/lisettedewilde/Assignment-3-Create-a-Web-API.git
```

2. Open project with Visual Studio

3. Using Entity Framework Core, run the following commands via the Package Manager Console:
```add-migration```  and  ```update-database```

4. Change the datasource of the  ```connectionstringbuilder``` in appsettings.json to connect with the server. 




## Contributors

[Lisette de Wilde (@lisettedewilde)](https://github.com/lisettedewilde)

[Murat Sahin (@m-sahin)](https://github.com/m-sahin)
