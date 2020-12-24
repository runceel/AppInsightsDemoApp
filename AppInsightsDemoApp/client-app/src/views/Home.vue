<template>
  <div class="home">
      <button class="uk-button uk-button-default" @click="updateLabel">ok</button>
      <input type="text" class="uk-input" v-model="state.label" />
    <span>{{ state.label }}</span>
  </div>
</template>

<script lang="ts">
import { ApplicationInsights } from "@microsoft/applicationinsights-web";
import { defineComponent, inject, reactive } from "vue"

type State = {
  label: string;
};

export default defineComponent({
  name: "Home",
  components: {
  },
  setup() {
    const appInsights = inject<ApplicationInsights>('appInsights') as ApplicationInsights
    const state = reactive<State>({
      label: "",
    })

    const updateLabel = () => {
      const newValue = new Date().toString()
      appInsights.trackEvent({ name: 'clicked', properties: { message: newValue } })
      appInsights.trackTrace({ message: 'クリックされた' })
      state.label = newValue
    }

    return {
      state,
      updateLabel,
    }
  },
});
</script>
