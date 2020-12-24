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
// 構成
const appInsights = new ApplicationInsights({
  config: {
    connectionString:
      process.env.VUE_APP_APPLICATION_INSIGHTS_CONNECTION_STRING,
    autoTrackPageVisitTime: true,
    enableAutoRouteTracking: true,
  },
});
// 情報収集開始
appInsights.loadAppInsights();

export default defineComponent({
  setup() {
    provide("appInsights", appInsights);
  },
});
</script>

<style>
</style>
