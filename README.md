# SUMMARY

Implemented one api named "/airport/getdistance"

GET `/airport/getdistance`

> Query Parameters
> `fromIata` : <IATA code name for source airport>
> `toIata` : <IATA code name for destination airport>


## Examples
Query parameters
```
{
  fromIata: "NHD",
  toIata: "AMS"
}
```

Response
```
3226.8495336697683
```
