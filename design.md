# WPF: разметка 

---

## DockPanel — «прибить» к краю, остаток заполнить

`Dock` задаёт сторону; **последний** дочерний элемент растягивается, если `LastChildFill="True"` (по умолчанию).

```xml
<DockPanel LastChildFill="True">
    <Button Content="Выход" DockPanel.Dock="Top"/>
    <StackPanel DockPanel.Dock="Bottom" Orientation="Horizontal">
        <Button Content="Ок"/>
    </StackPanel>
    <!-- всё место между Top и Bottom -->
    <ListBox/>
</DockPanel>
```

Порядок важен: сначала «вычитают» место пристыкованные, последним идёт тот, кто **заполняет** центр.

---

## Grid — строки и колонки

```xml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="160"/>   <!-- фикс -->
        <ColumnDefinition Width="*"/>     <!-- остаток -->
    </Grid.ColumnDefinitions>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="*"/>
    </Grid.RowDefinitions>
    <TextBox Grid.Row="0" Grid.Column="0"/>
    <TextBlock Grid.Row="0" Grid.Column="1" Text="Подпись"/>
</Grid>
```

`Auto` — по содержимому; `*` — доля свободного места; `2*` — вдвое шире соседа с `*`.

---

## StackPanel — столбик или ряд

```xml
<!-- столбик (по умолчанию) -->
<StackPanel>
    <TextBox/>
    <Button/>
</StackPanel>

<!-- в ряд -->
<StackPanel Orientation="Horizontal">
    <Button Content="Сохранить" Margin="0,0,8,0"/>
    <Button Content="Отмена"/>
</StackPanel>
```

Не растягивает детей на всю ширину родителя (если не задать `HorizontalAlignment` и т.д.).

---

## Border — отступ/рамка вокруг одного ребёнка

```xml
<Border Padding="16" BorderBrush="#CCC" BorderThickness="1" CornerRadius="4">
    <StackPanel>...</StackPanel>
</Border>
```

---

## Частый паттерн: строка формы (поле + подпись)

```xml
<Grid Margin="0,0,0,8">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="160"/>
        <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>
    <TextBox Grid.Column="0" Name="BoxName" Padding="6,4"/>
    <TextBlock Grid.Column="1" Text="Название" Margin="16,0,0,0" VerticalAlignment="Center"/>
</Grid>
```

---

## Окно

```xml
<Window ... WindowStartupLocation="CenterScreen"
        MinWidth="400" MinHeight="300" Width="600" Height="400">
```

---

## Отступы

- `Margin="8"` — со всех сторон; `Margin="0,0,0,8"` — только снизу.
- `Padding` — внутри элемента (кнопка, `Border`, `TextBox`).
