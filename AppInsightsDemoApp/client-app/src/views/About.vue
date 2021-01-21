<template>
  <div class="about">
    <Toast></Toast>
    <h1>This is an about page</h1>

    <div>
      error と送信するとエラーが発生。cache キャッシュの内容 でキャッシュのセットが出来ます。
    </div>

    <InputText v-model="state.input"></InputText>
    <Button @click="send">Send</Button>
    <br/>
    <Button @click="get">Get cache</Button>
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

type GetResponse = {
  cache: string;
}

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
      } catch (e) {
        appInsights.trackException({ error: e });
        toast.add({ severity: "error", summary: "エラーが発生しました。" });
      }
    };

    const get = async(e: Event) => {
      try {
        const getResponse = await axios.get<GetResponse>(`send?key=cache`);
        toast.add({ severity: "info", summary: "キャッシュ取得", detail: getResponse.data?.cache});
        console.log(JSON.stringify(getResponse.data));
      } catch (e) {
        appInsights.trackException({ error: e });
        toast.add({ severity: "error", summary: "エラーが発生しました。" });
      }
    };

    return {
      state,
      send,
      get,
    };
  },
});
</script>