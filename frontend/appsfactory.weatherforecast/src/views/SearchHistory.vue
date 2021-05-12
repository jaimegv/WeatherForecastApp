<template>
  <div class="searchHistory">
    <h1>Search History page</h1>
    <table class="table table-striped">
      <thead>
        <tr>
          <th>City</th>
          <th>Temperature</th>
          <th>Humidity</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in searchHistory" :key="item.City">
          <td scope="row">{{ item.City }}</td>  
          <td>{{ item.Temperature }} °C</td> 
          <td>{{ item.Humidity }} %</td>  
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import SearchHistoryEntry from "@/store/SearchHistoryEntry"
import store from "@/store";

export default ({
  data(): {
    searchHistory: SearchHistoryEntry[];
  } {
    return {
      searchHistory: store.state.searchHistory,
    };
  },
  methods: {
  },
});
</script>
<style>
</style>

