<template>
  <div class="main">
    <h1>Welcome to the Weather Forecast App</h1>
    <div class="container">
      <div class="row">
        <div class="col-sm">
          <form @submit.prevent="onSearchByCity">
            <div class="form-group row">
              <div class="col-md-10">
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
              <div class="col-md-10">
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
      <div v-for="item2 in weatherForecastList" :key="item2.Date">
        <div class="card bg-light mb-3" style="max-width: 18rem">
          <div class="card-header">{{ formatDate(item2.date) }}</div>
          <div class="card-body">
            <h5 class="card-title">Temperature {{ item2.temperature }} °C</h5>
            <h5 class="card-title">Humidity {{ item2.humidity }} %</h5>
            <h5 class="card-title">Wind Speed {{ item2.windspeed }} m/s</h5>
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

export default defineComponent({
  data(): {
    cityInput: string;
    zipCodeInput: string;
    backendUrl: string;
    weatherForecastList: [];
    listDefinition: string;
  } {
    return {
      backendUrl: process.env.VUE_APP_BackendUrl,
      cityInput: "",
      zipCodeInput: "",
      weatherForecastList: [],
      listDefinition: "",
    };
  },
  methods: {
    formatDate(date: Date): string {
      if (date) {
        console.log("antes:" + date);
        var localTime = moment.utc(date).toDate();
        console.log("despues:" + localTime);
        var dayName = moment(String(localTime)).format("dddd");
        console.log("dayName:" + dayName);
        var formattedDate = moment(String(localTime)).format("DD.MM.yyyy");
        console.log("dayName:" + formattedDate);
        return dayName + " " + formattedDate;
      } else {
        return "";
      }
    },
    async onSearchByCity() {
      this.weatherForecastList = [];
      if(!this.cityInput)
      {
        alert("Insert any German city name");
      } else {
        this.listDefinition = this.cityInput.toUpperCase();
        await this.getWeatherForecastData("city", this.cityInput);
      }
    },
    async onSearchByZipCode() {
      this.weatherForecastList = [];
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
        .get(this.backendUrl + "weather/forecast?" + paramName + "=" + value)
        .then((response) => {
          this.weatherForecastList = response.data;
        })
        .catch((error) => {
          alert(error.message);
          console.error(error.message);
        });
    },
  },
});
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
form {
  padding: 100;
}
.card {
  margin: 0 auto;
  float: none;
  margin-bottom: 10px;
}
</style>
