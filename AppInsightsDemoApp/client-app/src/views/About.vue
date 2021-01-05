<template>
  <div class="about">
    <Toast></Toast>
    <h1>This is an about page</h1>
    <InputText v-model="state.input"></InputText>
    <Button @click="send">Send</Button>
  </div>
</template>

<script lang="ts">
import { ApplicationInsights } from "@microsoft/applicationinsights-web";
import { defineComponent, inject, onMounted, reactive } from "vue";
import { useToast } from "primevue/usetoast";
import axios from "axios";

type State = {
  input: string;
};

export default defineComponent({
  setup() {
    const appInsights = inject<ApplicationInsights>(
      "appInsights"
    ) as ApplicationInsights;
    onMounted(() => appInsights.trackPageView({ name: 'About page' }));
    const toast = useToast();
    const state = reactive<State>({
      input: "",
    });

    const send = async (e: Event) => {
      try {
        await axios.post<string>("send", { input: state.input });
        toast.add({ severity: "info", summary: "送信できました。" });
        state.input = "";
      } catch (e) {
        appInsights.trackException({ error: e });
        toast.add({ severity: "error", summary: "エラーが発生しました。" });
      }
    };

    return {
      state,
      send,
    };
  },
});
</script>