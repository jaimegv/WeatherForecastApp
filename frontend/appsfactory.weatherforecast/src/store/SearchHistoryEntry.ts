export default class SearchHistoryEntry {
    public City: string;
    public Temperature: number;
    public Humidity: number;
  
    constructor(city: string, temperature: number, humidity: number) {
      this.City = city;
      this.Temperature = temperature;
      this.Humidity = humidity;
    }
  }