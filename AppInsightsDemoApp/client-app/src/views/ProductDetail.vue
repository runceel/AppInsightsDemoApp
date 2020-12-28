<template>
  <div>
    <Toast />
    <h2>Product</h2>

    <div v-if="state.product === undefined">Loading...</div>
    <div class="p-fluid" v-else>
      <div class="p-field">
        <label>Product number</label>
        <div>{{ state.product.productNumber }}</div>
      </div>
      <div class="p-field">
        <label>Product name</label>
        <div>{{ state.product.name }}</div>
      </div>
      <div class="p-field">
        <label>Standard cost</label>
        <div>{{ state.product.standardCost }}</div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Product } from "@/models/product";
import { ApplicationInsights } from "@microsoft/applicationinsights-web";
import axios from "axios";
import { useToast } from "primevue/usetoast";
import { defineComponent, inject, onMounted, reactive } from "vue";
import { useRoute } from "vue-router";

type State = {
  product?: Product;
};

export default defineComponent({
  setup() {
    const appInsights = inject<ApplicationInsights>(
      "appInsights"
    ) as ApplicationInsights;
    const route = useRoute();
    const productId = route.params.productId as string;
    const state = reactive<State>({ product: undefined });
    const toast = useToast();

    console.log(`ProductDetail.vue#setup: ${productId}`);
    console.log(`state: ${state.product}`);
    onMounted(async () => {
      console.log("ProductDetail.vue#onMounted");
      try {
        const result = await axios.get<Product>(`/product/${productId}`, {
          method: "get",
        });
        state.product = result.data;
        console.log("data loaded.");
      } catch (e) {
        appInsights.trackException({ error: e });
        toast.add({
          severity: "error",
          summary: "データの取得に失敗しました。",
        });
        state.product = undefined;
      }
    });

    return {
      state,
    };
  },
});
</script>
