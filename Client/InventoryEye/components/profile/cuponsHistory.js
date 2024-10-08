import { StyleSheet, Text, View, TouchableOpacity, Image  } from 'react-native'
import React from 'react'

//recives props (Number of bonus, bonus)
export default function CuponsHistory({  bonusPic,onBonusPress }) {
  return (
    <View>
      <TouchableOpacity onPress={onBonusPress}>
      {bonusPic && (
          <Image
            source={typeof bonusPic === 'string' ? { uri: bonusPic } : bonusPic}
            style={styles.bonusimage}
          />
        )}
      </TouchableOpacity>
    </View>
  )
}

const styles = StyleSheet.create({
  bonusimage: {
    width: 55,
    height: 55,
    borderRadius: 27.5,
    borderColor: '#111851',
    borderWidth: 1,
    marginLeft: '3%'
  },
})