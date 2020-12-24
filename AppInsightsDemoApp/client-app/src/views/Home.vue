<template>
  <div>
    <h2 class="">Form</h2>
    <div class="p-fluid">
      <div class="p-field">
        <label for="button">Set current date time to the below textbox</label>
        <Button id="button" class="p-d-block" label="Click" @click="updateLabel"
          >OK</Button
        >
      </div>

      <div class="p-field">
        <label for="textBox">Sample textbox</label>
        <InputText id="textBox" v-model="state.label" type="text" />
      </div>

      <div>
        <span id="message">{{ state.label }}</span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { ApplicationInsights } from "@microsoft/applicationinsights-web";
import { defineComponent, inject, reactive } from "vue";
import Button from "primevue/button";
import InputText from "primevue/inputtext";

type State = {
  label: string;
};

export default defineComponent({
  name: "Home",
  components: {
    Button,
    InputText,
  },
  setup() {
    const appInsights = inject<ApplicationInsights>(
      "appInsights"
    ) as ApplicationInsights;
    const state = reactive<State>({
      label: "",
    });

    const updateLabel = () => {
      const newValue = new Date().toString();
      appInsights.trackEvent({
        name: "clicked",
        properties: { message: newValue },
      });
      appInsights.trackTrace({ message: "クリックされた" });
      state.label = newValue;
    };

    return {
      state,
      updateLabel,
    };
  },
});
</script>
