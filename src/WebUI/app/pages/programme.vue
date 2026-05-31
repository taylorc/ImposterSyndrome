<template>
  <div class="container relative z-40 mx-auto mt-12">
    <div class="grid grid-cols-1 gap-12">
    <form class="*:m-2">
            <label class="block font-bold mb-2">Cycles</label>
            <select class="block border p-2 w-full">
                <option v-for="option in cycleOptions" :value="option.value">
                        {{option.text}}
                </option>
            </select>
    <input type="text">
            
</input>
<button class="block bg-blue-500 text-white p-2">Submit</button>
     
    </form>
    </div>
  </div>
</template>

<script lang="ts" setup>
const cycleOptions = ref([
  { text: 'One', value: 1 },
  { text: 'Two', value: 2 },
  { text: 'Three', value: 3 },
  { text: 'Four', value: 4 }
])

</script>

<style>

</style>