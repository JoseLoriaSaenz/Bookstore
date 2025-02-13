# Bookstore - **Project Type:** _API .Net Core_

## **Description**

This API .Net Core project hanldes some CRUD operations for books using a SQL Server database on a table named Books with the following structure:


    public class Book

    {

        public int Id { get; set; }
    
        public string Title { get; set; }
    
        public string Author { get; set; } 
    
        public string ISBN { get; set; }
    
        public DateTime PublishedDate { get; set; }
    
        public string Genre { get; set; }

    }

This project uses:
  1. EF for accessing the entities from the database.
  2. Implements a generic repository parttern.
  3. Includes a NUnit project for testing the UnitOfWork and the Repository classes.

## This API usage goes as follows:

## Method GET: Getbooks

  {apiUrl}//api/v1/Books/GetBooks 

## Method GET: GetBookById

  {apiUrl}/api/v1/Books/GetBookById?id=2

## Method GET: SearchBooks

  {apiUrl}//api/v1/Books/SearchBooks?query=Select%20%2A%20FROM%20books%20where%20Id%20%3D%202

## Method Post: AddBook

  {apiUrl}/api/v1/Books/AddBook passing a json on the body folloing this format:

    {
    
      "id": int,
      
      "title": "string",
      
      "author": "string",
      
      "isbn": "string",
      
      "publishedDate": "2025-02-13T17:10:06.849Z",
      
      "genre": "string"
      
    }

## Method DELETE: DeleteBook

  {apiUrl}/api/v1/Books/DeleteBook?id=8

