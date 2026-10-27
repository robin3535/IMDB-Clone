Feature: Actor Resource
Actor feature will testify the get actor,get actor by Id,create actor,delete and update actor.
@GetAllActors
Scenario: Get All Actors
	Given I am a client
	When I make GET Request '/actors'
	Then response code must be '200'
	And response data must look like from file 'C:\Users\Robin\Desktop\Actor.txt'

@GetActorById
Scenario Outline: Get Actor by Id 
	Given I am a client
	When I make GET Request '/actors' with parameter '<actorId>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples:
	| actorId | statusCode | responseData                                                                                               |
	| 1       | 200        | {"id":1,"name":"Shah Rukh Khan","bio":"He is a Superstar..","dob":"11/4/1965 12:00:00 AM","gender":"Male"} |
	| 5       | 404        | No Actor found with id= 5                                                                                  |

@AddActor
Scenario Outline: Add an Actor
	Given I am a client
	When I make POST Request '/actors' with body '<actorBody>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| actorBody                                                                                                 | statusCode | responseData            |
	| {"name":"Shah Rukh Khan","bio":"He is a superstar..","dob":"04-11-1965","gender":"male","updatedAt":null} | 201        | {"id":1}                |
	| {"name": "","Bio": "He is a superstar..","dob": "11-10-1942T00:00:00","gender": "Male"}                   | 400        | Name cannot be empty.   |
	| {"name": "Amitabh Bachchan","bio": "","dob": "11-10-1942T00:00:00","gender": "Male"}                      | 400        | Bio cannot be empty.    |
	| {"name": "Amitabh Bachchan","bio": "He is a superstar","dob": "11-10-1942T00:00:00","gender": ""}         | 400        | Gender cannot be empty. |
	| {"name": "Amitabh Bachchan","bio": "He is a superstar","dob": "","gender": "Male"}                        | 400        | DOB is empty/Invalid.   |


@UpdateActor
Scenario Outline: Update an Actor
	Given I am a client
	When I make PUT Request '/actors' with parameter '<actorId>' and body '<actorBody>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| actorId | actorBody                                                                                         | statusCode | responseData              |
	| 1       | {"name": "Amitabh Bachchan","bio": "He is a superstar..","dob": "11-10-1942","gender": "Male"}    | 200        | Updated Succesfully.      |
	| 1       | {"name": "","bio": "He is a superstar..","dob": "11-10-1942T00:00:00","gender": "Male"}           | 400        | Name cannot be empty.     |
	| 1       | {"name": "Amitabh Bachchan","bio": "","dob": "11-10-1942T00:00:00","gender": "Male"}              | 400        | Bio cannot be empty.      |
	| 1       | {"name": "Amitabh Bachchan","bio": "He is a superstar","dob": "11-10-1942T00:00:00","gender": ""} | 400        | Gender cannot be empty.   |
	| 1       | {"name": "Amitabh Bachchan","bio": "He is a superstar","dob": "","gender": "Male"}                | 400        | DOB is empty/Invalid.     |
	| 0       | {"name": "Amitabh Bachchan","bio": "He is a superstar","dob": "","gender": "Male"}                | 404        | No Actor found with id= 0 |

@DeleteActor
Scenario Outline: Delete an actor
	Given I am a client
	When I make DELETE Request '/actors' with parameter '<actorId>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| actorId | statusCode | responseData              |
	| 1       | 200        | Deleted Successfully.     |
	| 0       | 404        | No Actor found with id= 0 |