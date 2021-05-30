# service-log ![workflow](https://github.com/diyazy/service-log/actions/workflows/dotnetcore.yml/badge.svg) [![GitHub license](https://img.shields.io/github/license/DiyazY/service-log)](https://github.com/DiyazY/service-log/blob/dev/LICENSE)
Logging as a service. It's a very simple logging service that saves only logs and gives an ability to scan them.
The service is following DomainDrivenDesign, CQRS, Repository patterns. It looks like overengineering, but I do believe that a project will grow and these things will be necessary.  

docker image: `docker pull diyaz/service-log:latest`

## Using
For inspecting logs use Web UI, there is also searching form. 
Logs can be saved in several ways:

### Database
Certain workers (scrapers, consoles, whatever applications) can save logs directly to DB via sl.infrastructure assembly. The assembly has necessary validation logic to protect from invalid data.

### WebApi
Certain workers (scrapers, consoles, whatever applications) can send logs to the WebApi endpoint. There is a Swagger Api documentation, where models and methods are well described.

![image](https://user-images.githubusercontent.com/16912141/72687545-c9897b00-3b0f-11ea-9449-44a15b761f24.png)

## Examples

### Web UI
List of logs:
![image](https://user-images.githubusercontent.com/16912141/72686967-b7f1a480-3b0a-11ea-82be-b14d6331aec1.png)

Viewing a particular log:
![image](https://user-images.githubusercontent.com/16912141/72686995-ee2f2400-3b0a-11ea-90a2-e8cdde1f9080.png)

### Error on client side
Let's take an unhandled error as an example. Usually, when something goes wrong, application should notify a user about it. After that, the user see a message like "Something goes wrong!", then the user wants to understand what it was and, of course, avoid this kind of error in the future. Unfortunately, when people inform tech support about some bad behavior, most of them can't describe properly a problem and what they did. So consequently, the support gleans information from million logs and subjective client's description. It's not a fast and good way of solving the problem. However, what if a user can provide a link with the log that contains all the information: when? how? where? and stack trace :).

![image](https://user-images.githubusercontent.com/16912141/72687078-b1176180-3b0b-11ea-84f8-a690ab576dfb.png)

## License

service-log is [MIT licensed](./LICENSE).
