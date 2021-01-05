<template>
  <div id="nav">
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link>
  </div>
  <router-view />
</template>

<script lang="ts">
import { defineComponent, provide } from "vue";

export default defineComponent({
  setup() {
  },
});
</script>

<style>
</style>
