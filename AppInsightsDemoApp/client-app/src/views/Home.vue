<template>
  <div>
    <h2 class="">Data</h2>
    <div class="p-d-flex">
      <Button @click="updateLabel">Load</Button>
    </div>
      <DataTable :value="state.products" :paginator="true" class="p-datatable-sm"
        :rows="10"
        paginatorTemplate="CurrentPageReport FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink RowsPerPageDropdown"
        :rowsPerPageOptions="[10,20,50]"
        currentPageReportTemplate="Showing {first} to {last} of {totalRecords}">
        <Column field="productNumber" header="Product number"></Column>
        <Column field="name" header="Name"></Column>
      </DataTable>
  </div>
</template>

<script lang="ts">
import { ApplicationInsights } from "@microsoft/applicationinsights-web";
import { defineComponent, inject, reactive } from "vue";
import Button from "primevue/button";
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import axios from 'axios'
import { Product, Color } from '../models/product'

type State = {
  products: Product[];
};

export default defineComponent({
  name: "Home",
  components: {
    Button,
    DataTable,
    Column,
  },
  setup() {
    const appInsights = inject<ApplicationInsights>(
      "appInsights"
    ) as ApplicationInsights;
    const state = reactive<State>({
      products: [],
    });

    const updateLabel = async () => {
      const newValue = new Date().toString();
      appInsights.trackEvent({
        name: "clicked",
        properties: { message: newValue },
      });
      appInsights.trackTrace({ message: "クリックされた" });
      const productResult = await axios.get<Product[]>('product', {
        method: 'get'
      })
      if (productResult.status == 200) {
        state.products = productResult.data
        console.log(JSON.stringify(state.products[0]))
      }
    };

    return {
      state,
      updateLabel,
    };
  },
});
</script>
