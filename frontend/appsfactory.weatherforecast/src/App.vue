<template>
  <div class="main">
    <div class="span3 headerImg">
      <img src="./assets/umbrella.jpg" />
    </div>
    <div class="span3 imageCaption">
      Photo by
      <a
        target="_blank" rel="noopener noreferrer" href="https://unsplash.com/@wimvanteinde?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText"
      >
        Wim van 't Einde
      </a>
      on
      <a
        target="_blank" rel="noopener noreferrer" href="https://unsplash.com/collections/8350737/weather-forecast?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText"
      >
        Unsplash
      </a>
    </div>
  </div>
  <router-view/>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
#nav {
  padding: 30px;
}
#nav a {
  font-weight: bold;
  color: #2c3e50;
}
#nav a.router-link-exact-active {
  color: #42b983;
}
.headerImg img {
  width: 100%;
  height: 130px;
}
.footerImg img {
  width: 100%;
  height: 130px;
}
.imageCaption {
  height: 30px;
  font-size: 11px;
  text-align: right;
}
.footer { /* change selector otherwise this will select all bootstrap .col-sm-7.col-md-7 */
position: absolute; 
bottom: 0px; 
right: 0px;
}
</style>
