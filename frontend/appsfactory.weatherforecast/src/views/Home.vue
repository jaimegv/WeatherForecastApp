<template>
  <div class="main">
    <WeatherForecastMain />
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import WeatherForecastMain from "@/components/WeatherForecastMain.vue";

@Options({
  components: {
    WeatherForecastMain,
  },
})
export default class Home extends Vue {}
</script>
<style scoped lang="scss">
</style>

