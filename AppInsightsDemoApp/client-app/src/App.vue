<template>
  <div id="nav">
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link>
  </div>
  <router-view />
</template>

<script lang="ts">
import { defineComponent, provide } from "vue";
import { ApplicationInsights } from "@microsoft/applicationinsights-web";

export default defineComponent({
  setup() {
    const appInsights = new ApplicationInsights({
      config: {
        connectionString: process.env.VUE_APP_APPLICATION_INSIGHTS_CONNECTION_STRING,
        autoTrackPageVisitTime: true,
        enableAutoRouteTracking: true,
      },
    });
    appInsights.loadAppInsights();
    provide("appInsights", appInsights);
  },
});
</script>

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
</style>
