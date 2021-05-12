export default class WeatherForecastDto {
    public date: Date;
    public temperature: number;
    public humidity: number;
    public windSpeed: number;
  
    constructor() {
      this.date = new Date();
      this.temperature = 0;
      this.humidity = 0;
      this.windSpeed = 0;
    }
  }