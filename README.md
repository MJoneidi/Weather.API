# Weather.API
Forecast weather

# Requirments 
Create API endpoint(s) for retrieving the forecast by city or zipCode. a suggestion would be GET /api/weather/forecast?city=[cityName] and /api/weather/forecast?zipCode=[cityName]  
Those should return your own json payload (not just a mirror/passthrough of the OpenWeather JSON payload) containing the following data: average temperature and humidity per day build from the multiple values that are returned by the original api, beginning with today and next 5 days, at least containing temperature, humidity and wind speed
Make sure your returned json is refactoring-resilient, meaning a change to the model class property names shouldn't break the API

It is also your task to persist the current weather data each time a user queries weather data of a city in the browser. It is sufficient if you save the name of the city, the temperature and humidity. That should then appear in a separate "history" list in the UI.


# Description
I worked on VueJs and even implement connection to api and load the forcast api, but still I could not make it as good as I like it, I'd preffered to not upload it, sorry for inconvenience

# Run the project

All you need to do is running this command

```
docker-compose up
```

It will user port : 7001 
In case, these ports are not empty, you can change the default config in docker compose file
