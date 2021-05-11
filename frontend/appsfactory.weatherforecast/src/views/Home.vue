<template>
  <div class="main">
    <div class="span3 top1">
      <div class="row">
        <div class="span3 headerImg">
          <img src="../assets/nasa.jpg" />
        </div>
      </div>
    </div>
    <div class="span3 imageCaption">
      Photo by
      <a
        href="https://unsplash.com/@nasa?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText"
      >
        NASA
      </a>
      on
      <a
        href="https://unsplash.com/collections/8350737/weather-forecast?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText"
      >
        Unsplash
      </a>
    </div>

    <!--img alt="Vue logo" src="../assets/nasa2.jpg" /-->
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
.headerImg img {
  width: 100%;
  height: 130px;
}
.imageCaption {
  height: 30px;
  font-size: 11px;
  text-align: right;
}
</style>

