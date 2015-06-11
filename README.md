# Decode And Destroy
One time use website link creator to share passwords and other secret data.

https://decodeanddestroy.com

This is a ASP.NET MVC website that accepts plaintext, encrypts and stores that data in a SQL database, and then generates a website link to the plaintext content.

When a user access that link and retrieves the data, the data is removed from the database so the encrypted information can only ever be accessed once.  Users can also provide an additional password which requires the recipient of the link to also enter a password to retrieve and decrypt the data.

## Solution Description

-  SecretMessageWebsite is the ASP.NET website.
-  ManageDatabaseConsoleApp is an EXE that instructs the website to delete old messages.
