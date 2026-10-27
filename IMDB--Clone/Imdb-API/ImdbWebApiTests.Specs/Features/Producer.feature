Feature: Producer Resource
Producer feature will testify the get producer,get producer by Id, create producer,delete and update producer.


@GetAllProducers
Scenario: Get All Producers
	Given I am a client
	When I make GET Request '/producers'
	Then response code must be '200'
	And response data must look like from file 'C:\Users\Robin\Desktop\Producer.txt'

@GetProducerById
Scenario Outline: Get Producer by Id 
	Given I am a client
	When I make GET Request '/producers' with parameter '<producerId>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples:
	| producerId | statusCode | responseData                                                                           |
	| 1          | 200        | {"id":1,"name":"Karan","bio":"He is ..","dob":"10/4/1965 12:00:00 AM","gender":"Male"} |
	| 5          | 404        | No Producer found with id= 5                                                           |

@AddProducer
Scenario Outline: Add a Producer
	Given I am a client
	When I make POST Request '/producers' with body '<producerBody>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| producerBody                                                           | statusCode | responseData            |
	| {"name":"Karan","bio":"He is a ..","dob":"04-10-1965","gender":"Male"} | 201        | {"id":1}                |
	| {"name":"","bio":"He is a ..","dob":"04-10-1965","gender":"Male"}      | 400        | Name cannot be empty.   |
	| {"name":"Karan","bio":"","dob":"04-10-1965","gender":"Male"}           | 400        | Bio cannot be empty.    |
	| {"name":"Karan","bio":"He is a ..","dob":"04-10-19","gender":""}       | 400        | Gender cannot be empty. |
	| {"name":"Karan","bio":"He is a ..","dob":"","gender":"Male"}           | 400        | DOB is empty/Invalid.   |


@UpdateProducer
Scenario Outline: Update a Producer
	Given I am a client
	When I make PUT Request '/producers' with parameter '<producerId>' and body '<producerBody>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| producerId | producerBody                                                                     | statusCode | responseData                 |
	| 1          | {"name": "Karan johar","bio": "He is a ..","dob": "04-10-1963","gender": "Male"} | 200        | Updated Succesfully.         |
	| 1          | {"name": "","bio": "He is a ..","dob": "04-10-1963","gender": "Male"}            | 400        | Name cannot be empty.        |
	| 1          | {"name": "Karan johar","bio": "","dob": "04-10-1963","gender": "Male"}           | 400        | Bio cannot be empty.         |
	| 1          | {"name": "Karan johar","bio": "He is a ..","dob": "04-10-1963","gender": ""}     | 400        | Gender cannot be empty.      |
	| 1          | {"name": "Karan johar","bio": "He is a ..","dob": "","gender": "Male"}           | 400        | DOB is empty/Invalid.        |
	| 0          | {"name": "Karan johar","bio": "He is a ..","dob": "04-10-1963","gender": "Male"} | 404        | No Producer found with id= 0 |
                                                                        

@DeleteProducer
Scenario Outline: Delete a Producer
	Given I am a client
	When I make DELETE Request '/producers' with parameter '<producerId>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| producerId | statusCode | responseData                 |
	| 1          | 200        | Deleted Successfully.        |
	| 0          | 404        | No Producer found with id= 0 |