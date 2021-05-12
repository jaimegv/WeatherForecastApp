<template>
  <div class="main">
    <h1>Germany cities Weather Forecast App</h1>
    <div class="container">
      <div class="row">
        <div class="col-sm">
          <form @submit.prevent="onSearchByCity">
            <div class="form-group row">
              <div class="col-md-6">
                <label class="label" for="city">Search by city name</label>
                <input
                  type="text"
                  class="form-control"
                  id="city"
                  placeholder="city name"
                  v-model="cityInput"
                />
                <input
                  type="submit"
                  value="Search by city"
                  class="btn btn-success"
                />
              </div>
            </div>
          </form>
        </div>
        <div class="col-sm">
          <form @submit.prevent="onSearchByZipCode">
            <div class="form-group row">
              <div class="col-md-6">
                <label class="label" for="zipCode">Search by zip Code</label>
                <input
                  type="number"
                  class="form-control"
                  id="zipCode"
                  placeholder="zip code"
                  v-model="zipCodeInput"
                />
                <input
                  type="submit"
                  value="Search by zip code"
                  class="btn btn-success"
                />
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
    <div v-if="weatherForecastList && weatherForecastList.length > 0">
      <h4>{{ listDefinition }} next days prediction</h4>
      <hr />
      <div v-for="item in weatherForecastList" :key="item.date">
        <div class="card bg-light mb-3" style="max-width: 18rem">
          <div class="card-header">{{ formatDate(item.date) }}</div>
          <div class="card-body">
            <h5 class="card-title">Temperature {{ item.temperature }} °C</h5>
            <h5 class="card-title">Humidity {{ item.humidity }} %</h5>
            <h5 class="card-title">Wind Speed {{ item.windspeed }} m/s</h5>
          </div>
        </div>
      </div>
    </div>
    <div id="nav">
      <router-link to="/SearchHistory">Search History</router-link>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import axios from "axios";
import moment from "moment";
import WeatherForecastDto from "./WeatherForecastDto";
import SearchHistoryEntry from "@/store/SearchHistoryEntry";
import store from "@/store";

export default defineComponent({
  data(): {
    cityInput: string;
    zipCodeInput: string;
    backendUrl: string;
    weatherForecastList: Array<WeatherForecastDto>;
    listDefinition: string;
  } {
    return {
      backendUrl: process.env.VUE_APP_BackendUrl,
      cityInput: "",
      zipCodeInput: "",
      weatherForecastList: new Array<WeatherForecastDto>(),
      listDefinition: "",
    };
  },
  methods: {
    formatDate(date: Date): string {
      if (date) {
        var localTime = moment.utc(date).toDate();
        var dayName = moment(String(localTime)).format("dddd");
        var formattedDate = moment(String(localTime)).format("DD.MM.yyyy");
        return dayName + " " + formattedDate;
      } else {
        return "";
      }
    },
    async onSearchByCity() {
      this.weatherForecastList = new Array<WeatherForecastDto>();
      if(!this.cityInput)
      {
        alert("Insert any German city name");
      } else {
        this.listDefinition = this.cityInput.toUpperCase();
        await this.getWeatherForecastData("city", this.cityInput);
      }
    },
    async onSearchByZipCode() {
      this.weatherForecastList = new Array<WeatherForecastDto>();
      if(!this.zipCodeInput)
      {
        alert("Insert any German valid zip code");
      } else {
        this.listDefinition = this.zipCodeInput + " zip code";
        await this.getWeatherForecastData("zipCode", this.zipCodeInput);
      }
    },
    async getWeatherForecastData(paramName: string, value: string) {
      axios
        .get<Array<WeatherForecastDto>>(this.backendUrl + "weather/forecast?" + paramName + "=" + value)
        .then((response) => {
          this.weatherForecastList = response.data;
        })
        .then (() => {
          if(this.weatherForecastList && this.weatherForecastList.length > 0)
          {
            this.storeSearchHistoryEntry(value);
          }
        })
        .catch((error) => {
          if(error.response.status === 404)
          {
            alert("Not found: " + value);
          }
          else{
            alert("Fail, try it later ;)");
          }
        });
    },
    storeSearchHistoryEntry(value: string) {
      var todatyData = this.weatherForecastList[0];
      if(todatyData) {
        store.dispatch("addSearchHistoryEntry", new SearchHistoryEntry(value, todatyData.temperature, todatyData.humidity));
      }
    },
  },
});
</script>

<style scoped lang="scss">
.card {
  margin: 0 auto;
  float: none;
  margin-bottom: 10px;
}
nav li:hover,
nav li.router-link-active,
nav li.router-link-exact-active {
  cursor: pointer; 
}
#nav a{
  color: blue;
}
button, input{
  margin-top: 5px;
  margin-bottom: 5px;
  margin-left: 5px;
  margin-right: 5px;
}
</style>


