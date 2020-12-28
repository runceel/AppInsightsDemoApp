<template>
  <div>
    <Toast />
    <h2 class="">Data</h2>
    <div class="p-d-flex">
      <Button @click="loadData">Load</Button>
    </div>
    <DataTable
      :value="state.products"
      :paginator="true"
      class="p-datatable-sm"
      selectionMode="single"
      dataKey="productId"
      :rows="10"
      paginatorTemplate="CurrentPageReport FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink RowsPerPageDropdown"
      @row-select="onRowSelect"
      :rowsPerPageOptions="[10, 20, 50]"
      currentPageReportTemplate="Showing {first} to {last} of {totalRecords}"
    >
      <Column field="productNumber" header="Product number"></Column>
      <Column field="name" header="Name"></Column>
    </DataTable>
  </div>
</template>

<script lang="ts">
import {
  ApplicationInsights,
  SeverityLevel,
} from "@microsoft/applicationinsights-web";
import { defineComponent, inject, onMounted, reactive } from "vue";
import Button from "primevue/button";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import { useToast } from "primevue/usetoast";
import axios from "axios";
import { Product } from "../models/product";
import { useRouter } from "vue-router";

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
    const toast = useToast();
    const appInsights = inject<ApplicationInsights>(
      "appInsights"
    ) as ApplicationInsights;
    const state = reactive<State>({
      products: [],
    });

    const loadData = async () => {
      try {
        const newValue = new Date().toString();
        appInsights.trackEvent({
          name: "clicked",
          properties: { message: newValue },
        });
        appInsights.trackTrace({ message: "クリックされた" });
        const productResult = await axios.get<Product[]>("product", {
          method: "get",
        });
        if (productResult.status == 200) {
          state.products = productResult.data;
          appInsights.trackTrace({
            message: `${state.products.length} 件のデータを取得しました。`,
            severityLevel: SeverityLevel.Information,
          });

          toast.add({
            severity: "success",
            summary: "Data loaded.",
            detail: `${state.products.length} 件のデータを読み込みました。`,
            life: 3000,
          });
        } else {
          appInsights.trackTrace({
            message: `データの取得が出来ませんでした。`,
            severityLevel: SeverityLevel.Error,
          });
        }
      } catch (e) {
        appInsights.trackException({
          error: e,
        });
        toast.add({
          severity: "error",
          summary: "データの取得に失敗しました。",
        });
      }
    };

    const router = useRouter();
    onMounted(async() => {
      await loadData();
    });
    const onRowSelect = async (e: Event) => {
      const data = (<any>e).data as Product;
      await router.push({
        name: 'productDetail',
        params: { productId: data.productId },
      });
    };

    return {
      state,
      loadData,
      onRowSelect,
    };
  },
});
</script>
