Feature: Movie Resource
Movie feature will testify the get movie,get movie by id,create movie,delete and update movie.
@GetAllMovies
Scenario: Get All Movies
	Given I am a client
	When I make GET Request '/movies'
	Then response code must be '200'
	And response data must look like from file 'C:\Users\Robin\Desktop\Movie.txt'

@GetMovieById
Scenario Outline: Get Movie by Id 
	Given I am a client
	When I make GET Request '/movies' with parameter '<movieId>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples:
	| movieId | statusCode | responseData                                                                                                                                                                                                                                                                                                                                                                           |
	| 1       | 200        | {"id":1,"name":"Pathaan","yor":2022,"plot":"It is...","language":"Hindi","profit":100000,"actors":[{"id":1,"name":"Shah Rukh Khan","bio":"He is a Superstar..","dob":"11/4/1965 12:00:00 AM","gender":"Male"}],"genres":[{"id":1,"name":"Drama"}],"producer":{"id":1,"name":"Karan","bio":"He is ..","dob":"10/4/1965 12:00:00 AM","gender":"Male"},"coverImage":"https//www.img.png"} |
	| 5       | 404        | No Movie found with id= 5                                                                                                                                                                                                                                                                                                                                                              |

@AddMovie
Scenario Outline: Add a Movie
	Given I am a client
	When I make POST Request '/movies' with body '<movieBody>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| movieBody                                                                                                                                                     | statusCode | responseData              |
	| {"Name":"Pathaan","Plot":"It is...","Yor":2022,"Actors":"1","Genres":"1","Profit":100000,"Language":"Hindi","ProducerId":1,"CoverImage":"https//www.img.png"} | 201        | {"id":1}                  |
	| {"Name":"","Plot":"It is...","Yor":2022,"Actors":"1","Genres":"1","Profit":100000,"Language":"Hindi","ProducerId":1,"CoverImage":"https//www.img.png"}        | 400        | Name cannot be empty.     |
	| {"Name":"Pathaan","Plot":"","Yor":2022,"Actors":"1","Genres":"1","Profit":100000,"Language":"Hindi","ProducerId":1,"CoverImage":"https//www.img.png"}         | 400        | Plot cannot be empty.     |
	| {"Name":"Pathaan","Plot":"It is...","Yor":2024,"Actors":"1","Genres":"1","Profit":100000,"Language":"Hindi","ProducerId":1,"CoverImage":"https//www.img.png"} | 400        | Invalid Year of release.  |
	| {"Name":"Pathaan","Plot":"It is...","Yor":2022,"Actors":"1","Genres":"1","Profit":100000,"Language":"","ProducerId":1,"CoverImage":"https//www.img.png"}      | 400        | Language cannot be empty. |


@UpdateMovie
Scenario Outline: Update a Movie
	Given I am a client
	When I make PUT Request '/movies' with parameter '<movieId>' and body '<movieBody>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| movieId | movieBody                                                                                                                                                     | statusCode | responseData              |
	| 1       | {"Name":"Pathaan","Plot":"It is...","Yor":2022,"Actors":"1","Genres":"1","Profit":100000,"Language":"Hindi","ProducerId":1,"CoverImage":"https//www.img.png"} | 200        | Updated Succesfully.      |
	| 1       | {"Name":"","Plot":"It is...","Yor":2022,"Actors":"1","Genres":"1","Profit":100000,"Language":"Hindi","ProducerId":1,"CoverImage":"https//www.img.png"}        | 400        | Name cannot be empty.     |
	| 1       | {"Name":"Pathaan","Plot":"","Yor":2022,"Actors":"1","Genres":"1","Profit":100000,"Language":"Hindi","ProducerId":1,"CoverImage":"https//www.img.png"}         | 400        | Plot cannot be empty.     |
	| 1       | {"Name":"Pathaan","Plot":"It is...","Yor":2024,"Actors":"1","Genres":"1","Profit":100000,"Language":"Hindi","ProducerId":1,"CoverImage":"https//www.img.png"} | 400        | Invalid Year of release.  |
	| 1       | {"Name":"Pathaan","Plot":"It is...","Yor":2022,"Actors":"1","Genres":"1","Profit":100000,"Language":"","ProducerId":1,"CoverImage":"https//www.img.png"}      | 400        | Language cannot be empty. |
	| 0       | {"Name":"Pathaan","Plot":"It is...","Yor":2022,"Actors":"1","Genres":"1","Profit":100000,"Language":"","ProducerId":1,"CoverImage":"https//www.img.png"}      | 404        | No Movie found with id= 0 |

@DeleteMovie
Scenario Outline: Delete a Movie
	Given I am a client
	When I make DELETE Request '/movies' with parameter '<movieId>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| movieId | statusCode | responseData              |
	| 1       | 200        | Deleted Successfully.     |
	| 0       | 404        | No Movie found with id= 0 |